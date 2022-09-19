using UniversityApiBackend.Model.DataModels;

namespace UniversityApiBackend.Services
{
    public interface ICategoriesService
    {
        public IEnumerable<Course> GetAllCoursesOfCategory(IEnumerable<Category> categories, int id);
    }
}
