using UniversityApiBackend.Model.DataModels;

namespace UniversityApiBackend.Services
{
    public class CategoriesService : ICategoriesService
    {
        public IEnumerable<Course> GetAllCoursesOfCategory(IEnumerable<Category> categories, int id)
        {
            Category? category = categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
                return category.Courses;

            return new List<Course>();
        }
    }
}
