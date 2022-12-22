using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.Domain.Model
{
  public class Discipline
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Time { get; set; }
    //Свойства навигации
    public List<Lesson> Lessons { get; set; } = new List<Lesson>();
    public void AddLesson(Lesson lesson)
    {
      Lessons.Add(lesson);
    }
    public void RemoveLesson(int index)
    {
      Lessons.RemoveAt(index);
    }
    public int LessonCount { get { return Lessons.Count; } }
  }
}
