using FreeEDU.Model.Course;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace FreeEDU.Core.Serializers
{
	static class CourseCollectionSerializer
	{
		public static void Serialize(ObservableCollection<Course> courses)
			=> JsonSerializer.Serialize(courses);
		

		public static ObservableCollection<Course> Deserialize(string json)
			=> (json != null && json != string.Empty) ? JsonSerializer.Deserialize<ObservableCollection<Course>>(json) : new ObservableCollection<Course>();
	}
}
