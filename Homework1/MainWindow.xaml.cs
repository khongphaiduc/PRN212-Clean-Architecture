using System;
using System.Windows;
using System.Windows.Controls;
using Homework1.Applicationss.DTOs;
using Homework1.Applicationss.Interfaces;
using Homework1.Infastructure.Models;
using Homework1.Infastructure.Repo;
using Homework1.Infastructure.ServiceImplement;

namespace Homework1
{
    public partial class MainWindow : Window
    {
        private readonly IProjectService _projectService;

        public MainWindow()
        {
            InitializeComponent();

            var context = new CompanyDbContext();
            var projectRepo = new ProjectRepository(context);
            var employeeRepo = new EmployeeRepository(context);
            var empProjectRepo = new EmployeeProjectRepository(context);

            _projectService = new ProjectService(projectRepo, employeeRepo, empProjectRepo);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProjectList();
            LoadAllEmployeesToCombo();
        }

        private void LoadProjectList()
        {
            try
            {
                dgProjects.ItemsSource = _projectService.GetAllProjects();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadAllEmployeesToCombo()
        {
            try
            {
                cbEmployees.ItemsSource = _projectService.GetAllEmployeesOrderedByName();

              
                cbEmployees.DisplayMemberPath = "FullName";
                cbEmployees.SelectedValuePath = "EmployeeId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách nhân viên: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dgProjects.SelectedItem is ProjectDTO selectedProject)
            {
                LoadEmployeesInProject(selectedProject.ProjectId);
            }
            else
            {
                ClearDetails();
            }
        }

        private void LoadEmployeesInProject(Guid projectId)
        {
            try
            {
                dgProjectEmployees.ItemsSource = _projectService.GetEmployeesInProject(projectId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thành viên dự án: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (!(dgProjects.SelectedItem is ProjectDTO selectedProject))
            {
                MessageBox.Show("Vui lòng chọn một dự án từ danh sách bên trái trước!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cbEmployees.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên muốn thêm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Guid selectedEmployeeId = (Guid)cbEmployees.SelectedValue;
            string role = txtRole.Text.Trim();

            try
            {
                bool isSuccess = await _projectService.AddEmployeeToProjectAsync(selectedProject.ProjectId, selectedEmployeeId, role);

                if (!isSuccess)
                {
                    MessageBox.Show("Nhân viên này đã tham gia vào dự án rồi!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                MessageBox.Show("Thêm nhân viên vào dự án thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                txtRole.Clear();
                LoadEmployeesInProject(selectedProject.ProjectId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nhân viên vào dự án: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearDetails()
        {
            cbEmployees.SelectedIndex = -1;
            txtRole.Clear();
            dgProjectEmployees.ItemsSource = null;
        }
    }
}