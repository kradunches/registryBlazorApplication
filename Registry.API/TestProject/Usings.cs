global using Xunit;
using Registry.Domain.Model;
using TestProject;

public class TestPersonRepository
{
  [Fact]
  //Тест, проверяющий, что база данных создалась
  public void VoidTest()
  {
    var testHelper = new TestHelper();

    var disciplineRepository = testHelper.DisciplineRepository;

    Assert.Equal(1, 1);
  }

  [Fact]
  public void TestAdd()
  {
    var testHelper = new TestHelper();
    var disciplineRepository = testHelper.DisciplineRepository;

    var discipline2 = new Discipline
    {
      Name = "Математика",
      Time = 200
    };

    disciplineRepository.AddAsync(discipline2).Wait();
    //Запрещаем отслеживание сущностей (разрываем связи с БД)
    disciplineRepository.ChangeTrackerClear();

    Assert.True(disciplineRepository.GetAllAsync().Result.Count == 2);
    Assert.Equal("Математика", disciplineRepository.GetByNameAsync("Математика").Result.Name);
    Assert.Equal("Информатика", disciplineRepository.GetByNameAsync("Информатика").Result.Name);
  }

  [Fact]
  public void TestUpdate()
  {
    var testHelper = new TestHelper();
    var disciplineRepository = testHelper.DisciplineRepository;
    var discipline = disciplineRepository.GetByNameAsync("Информатика").Result;
    //Запрещаем отслеживание сущностей (разрываем связи с БД)
    disciplineRepository.ChangeTrackerClear();
    discipline.Name = "Проектирование";
    disciplineRepository.UpdateAsync(discipline).Wait();

    Assert.Equal("Проектирование", disciplineRepository.GetByNameAsync("Проектирование").Result.Name);


  }


  [Fact]
  public void TestDelete()
  {
    var testHelper = new TestHelper();
    var disciplineRepository = testHelper.DisciplineRepository;
    var discipline = disciplineRepository.GetByNameAsync("Информатика").Result;
    //Запрещаем отслеживание сущностей (разрываем связи с БД)
    disciplineRepository.ChangeTrackerClear();

    disciplineRepository.DeleteAsync(discipline.Id).Wait();




    Assert.True(disciplineRepository.GetAllAsync().Result.Count == 0);


  }

  [Fact]
  public void TestUpdateAdd()
  {
    var testHelper = new TestHelper();
    var disciplineRepository = testHelper.DisciplineRepository;
    var discipline = disciplineRepository.GetByNameAsync("Информатика").Result;
    //Запрещаем отслеживание сущностей (разрываем связи с БД)
    disciplineRepository.ChangeTrackerClear();
    discipline.Name = "Математика";
    var lessons = new Lesson
    {
      Topic = "3 курс",
      LessonType = "Лекция",
    };
    discipline.AddLesson(lessons);

    disciplineRepository.UpdateAsync(discipline).Wait();

    Assert.Equal("Математика", disciplineRepository.GetByNameAsync("Математика").Result.Name);
    Assert.Equal(1, disciplineRepository.GetByNameAsync("Математика").Result.LessonCount);
  }

  [Fact]
  public void TestUpdateDelete()
  {
    var testHelper = new TestHelper();
    var disciplineRepository = testHelper.DisciplineRepository;
    var discipline = disciplineRepository.GetByNameAsync("Информатика").Result;
    //Запрещаем отслеживание сущностей (разрываем связи с БД)
    disciplineRepository.ChangeTrackerClear();
    var lessons = new Lesson
    {
      Topic = "2 курс",
      LessonType = "Практическое занятие",
    };
    discipline.AddLesson(lessons);
    discipline.RemoveLesson(0);

    disciplineRepository.UpdateAsync(discipline).Wait();

    Assert.Equal(0, disciplineRepository.GetByNameAsync("Информатика").Result.LessonCount);
  }

}