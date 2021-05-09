using FreeEDU.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
