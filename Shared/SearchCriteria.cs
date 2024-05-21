namespace FullProject.Shared
{
    public class SearchCriteria
    {
        public string SearchTerm { get; set; }
        public string SelectedJobTitle { get; set; }
        public string SelectedDepartment { get; set; }

        public string ThangChamCong { get; set; } // Thêm thuộc tính mới để lưu trữ tháng chấm công
    }
}
