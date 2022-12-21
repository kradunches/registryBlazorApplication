global using Xunit;
using Registry.Domain.Model;
using TestProject;

public class TestPersonRepository
{
  [Fact]
  //����, �����������, ��� ���� ������ ���������
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
      Name = "����������",
      Time = 200
    };

    disciplineRepository.AddAsync(discipline2).Wait();
    //��������� ������������ ��������� (��������� ����� � ��)
    disciplineRepository.ChangeTrackerClear();

    Assert.True(disciplineRepository.GetAllAsync().Result.Count == 2);
    Assert.Equal("����������", disciplineRepository.GetByNameAsync("����������").Result.Name);
    Assert.Equal("����������������", disciplineRepository.GetByNameAsync("����������������").Result.Name);
  }

  [Fact]
  public void TestUpdate()
  {
    var testHelper = new TestHelper();
    var disciplineRepository = testHelper.DisciplineRepository;
    var discipline = disciplineRepository.GetByNameAsync("����������������").Result;
    //��������� ������������ ��������� (��������� ����� � ��)
    disciplineRepository.ChangeTrackerClear();
    discipline.Name = "��������������";
        discipline.AddLesson(new Lesson
        {
            Topic = "Name",
            LessonType = "������",
        });
        disciplineRepository.UpdateAsync(discipline).Wait();

    Assert.Equal("��������������", disciplineRepository.GetByNameAsync("��������������").Result.Name);
    }


  [Fact]
  public void TestDelete()
  {
    var testHelper = new TestHelper();
    var disciplineRepository = testHelper.DisciplineRepository;
    var discipline = disciplineRepository.GetByNameAsync("����������������").Result;
    //��������� ������������ ��������� (��������� ����� � ��)
    disciplineRepository.ChangeTrackerClear();

    disciplineRepository.DeleteAsync(discipline.Id).Wait();




    Assert.True(disciplineRepository.GetAllAsync().Result.Count == 0);


  }

  [Fact]
  public void TestUpdateAdd()
  {
    var testHelper = new TestHelper();
    var disciplineRepository = testHelper.DisciplineRepository;
    var discipline = disciplineRepository.GetDisciplineAsync("����������������").Result;
    //��������� ������������ ��������� (��������� ����� � ��)
    disciplineRepository.ChangeTrackerClear();
    discipline.Name = "����������";
        discipline.AddLesson(new Lesson
        {
            Topic = "�����",
            LessonType = "������������"
        });

        disciplineRepository.UpdateAsync(discipline).Wait();

    Assert.Equal("����������", disciplineRepository.GetDisciplineAsync("����������").Result.Name);
    Assert.Equal(2, disciplineRepository.GetDisciplineAsync("����������").Result.LessonCount);
  }

  [Fact]
  public void TestUpdateDelete()
  {
    var testHelper = new TestHelper();
    var disciplineRepository = testHelper.DisciplineRepository;
    var discipline = disciplineRepository.GetByNameAsync("����������������").Result;
    //��������� ������������ ��������� (��������� ����� � ��)
    disciplineRepository.ChangeTrackerClear();
    var lessons = new Lesson
    {
      Topic = "2 ����",
      LessonType = "������������ �������",
    };
    discipline.AddLesson(lessons);
    discipline.RemoveLesson(0);

    disciplineRepository.UpdateAsync(discipline).Wait();

    Assert.Equal(0, disciplineRepository.GetByNameAsync("����������������").Result.LessonCount);
  }

}