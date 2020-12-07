using BusinessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
    public class Program
    {

        static BusinessLayer.BusinessLayer bl = new BusinessLayer.BusinessLayer();

        static void Main(string[] args)
        {
            char choice;

            for (; ; )
            {
                do
                {
                    Console.WriteLine("Teacher or Courses:");
                    Console.WriteLine("  1. Teacher");
                    Console.WriteLine("  2. Courses");
                    Console.Write("Choose one (q to quit): ");
                    do
                    {
                        choice = Console.ReadLine()[0];
                    } while (choice == '\n' | choice == '\r');
                } while (choice < '1' | choice > '2' & choice != 'q');

                if (choice == 'q') break;

                Console.WriteLine("\n");

                switch (choice)
                {
                    case '1':
                        showTeacherMenu();
                        break;
                    case '2':
                        showCourseMenu();
                        break;
                }
            }
        }

        private static void showCourseMenu()
        {
            char choice;

            for (; ; )
            {
                do
                {
                    Console.WriteLine("Options:");
                    Console.WriteLine("  1. Create");
                    Console.WriteLine("  2. Update");
                    Console.WriteLine("  3. Delete");
                    Console.WriteLine("  4. List all courses");
                    Console.WriteLine("  5. Change teacher of course");
                    Console.WriteLine("  6. Create course and teacher");
                    Console.Write("Choose one (q to quit): ");
                    do
                    {
                        choice = Console.ReadLine()[0];
                    } while (choice == '\n' | choice == '\r');
                } while (choice < '1' | choice > '6' & choice != 'q');

                if (choice == 'q') break;

                Console.WriteLine("\n");

                switch (choice)
                {
                    case '1':
                        AddCourse();
                        break;
                    case '2':
                        UpdateCourse();
                        break;
                    case '3':
                        DeleteCourse();
                        break;
                    case '4':
                        ListAllCourses();
                        break;
                    case '5':
                        ChangeTeacherOfCourse();
                        break;
                    case '6':
                        CreateCourseAndTeacher();
                        break;
                }
            }
        }

        private static void showTeacherMenu()
        {
            char choice;

            for (; ; )
            {
                do
                {
                    Console.WriteLine("Options:");
                    Console.WriteLine("  1. Create");
                    Console.WriteLine("  2. Update");
                    Console.WriteLine("  3. Delete");
                    Console.WriteLine("  4. List all teachers");
                    Console.WriteLine("  5. List courses of teacher");
                    Console.WriteLine("  6. List all standards");
                    Console.Write("Choose one (q to quit): ");
                    do
                    {
                        choice = Console.ReadLine()[0];
                    } while (choice == '\n' | choice == '\r');
                } while (choice < '1' | choice > '6' & choice != 'q');

                if (choice == 'q') break;

                Console.WriteLine("\n");

                switch (choice)
                {
                    case '1':
                        AddTeacher();
                        break;
                    case '2':
                        UpdateTeacher();
                        break;
                    case '3':
                        DeleteTeacher();
                        break;
                    case '4':
                        ListAllTeachers();
                        break;
                    case '5':
                        GetCourseByTeacherID();
                        break;
                    case '6':
                        GetAllStandards();
                        break;
                }
            }
        }

        private static void GetAllStandards()
        {
            Console.Write("All Standards \n ");

            IEnumerable<Standard> list = bl.GetAllStandards();
            if (list == null || list.Count() == 0)
            {
                Console.WriteLine("List has no standards!");
                return;
            }

            foreach (Standard i in list)
                Console.WriteLine("- " + i.StandardName + " | sID: " + i.StandardId);
        }

        private static void ListAllCourses()
        {
            Console.Write("All Courses \n ");

            IEnumerable<Course> list = bl.GetAllCourses();
            if (list == null || list.Count() == 0)
            {
                Console.WriteLine("List has no courses!");
                return;
            }

            foreach (Course i in list)
                Console.WriteLine("- " + i.CourseName + " | cID: " + i.CourseId + " | tID: " + i.TeacherId);
        }

        private static void AddCourse()
        {
            Console.WriteLine("- CREATE COURSE -");
            Console.WriteLine("Enter Course Name: ");
            string courseName = Console.ReadLine();
            Console.WriteLine("Enter Teacher ID: ");
            int teacherID = Convert.ToInt32(Console.ReadLine());

            Course c1;

            if(teacherID == 0)
            {
                c1 = new Course
                {
                    TeacherId = null,
                    CourseName = courseName
                };
            } else
            {
                if (bl.GetTeacherByID(teacherID) == null)
                {
                    Console.WriteLine("Teacher not found!");
                    return;
                }

                c1 = new Course
                {
                    TeacherId = teacherID,
                    CourseName = courseName
                };
            }

            bl.AddCourse(c1);
        }

        private static void UpdateCourse()
        {
            Console.WriteLine("- UPDATE COURSE -");
            Console.WriteLine("Enter Course ID: ");
            int courseID = Convert.ToInt32(Console.ReadLine());

            var c1 = bl.GetCourseByID(courseID);
            if(c1 == null)
            {
                Console.WriteLine("Course not found!");
                return;
            }

            Console.WriteLine("Enter New Course Name: ");
            string courseName = Console.ReadLine();

            c1.CourseName = courseName;
            bl.UpdateCourse(c1);
        }

        private static void DeleteCourse()
        {
            Console.WriteLine("- DELETE COURSE -");
            Console.WriteLine("Enter Course ID: ");
            int courseID = Convert.ToInt32(Console.ReadLine());

            var c1 = bl.GetCourseByID(courseID);
            if (c1 == null)
            {
                Console.WriteLine("Course not found!");
                return;
            }

            bl.RemoveCourse(c1);
        }

        private static void AddTeacher()
        {
            string teacherName;
            int standardID;

            Console.WriteLine("- CREATE - ");
            Console.WriteLine("Enter Teacher Name: ");
            teacherName = Console.ReadLine();
            Console.WriteLine("Enter Standard ID: ");
            standardID = Convert.ToInt32(Console.ReadLine());

            if (bl.GetStandardByID(standardID) == null)
            {
                Console.WriteLine("Stanard not found!");
                return;
            }

            Teacher t1 = new Teacher
            {
                TeacherName = teacherName,
                StandardId = standardID
            };

            bl.AddTeacher(t1);
        }

        private static void UpdateTeacher()
        {
            Console.WriteLine("- UPDATE TEACHER -");
            Console.WriteLine("Enter Teacher ID: ");
            int teacherID = Convert.ToInt32(Console.ReadLine());

            var t1 = bl.GetTeacherByID(teacherID);
            if (t1 == null)
            {
                Console.WriteLine("Teacher not found!");
                return;
            }

            Console.WriteLine("Enter New Teacher Name: ");
            string teacherName = Console.ReadLine();

            t1.TeacherName = teacherName;
            bl.UpdateTeacher(t1);
        }

        private static void DeleteTeacher()
        {
            Console.WriteLine("- DELETE TEACHER -");
            Console.WriteLine("Enter Teacher ID: ");
            int teacherID = Convert.ToInt32(Console.ReadLine());

            var t1 = bl.GetTeacherByID(teacherID);
            if (t1 == null)
            {
                Console.WriteLine("Teacher not found!");
                return;
            }

            bl.RemoveTeacher(t1);
        }

        private static void ListAllTeachers()
        {
            Console.Write("All Teachers \n ");

            IEnumerable<Teacher> list = bl.GetAllTeachers();
            if (list == null || list.Count() == 0)
            {
                Console.WriteLine("List has no teachers!");
                return;
            }

            foreach (Teacher i in list)
                Console.WriteLine("- " + i.TeacherName + " | tID: " + i.TeacherId + " | sID: " + i.StandardId);

        }

        private static void GetCourseByTeacherID()
        {
            Console.Write("Enter Teacher ID: ");
            int teacherID = Convert.ToInt32(Console.ReadLine());

            if (bl.GetTeacherByID(teacherID) == null)
            {
                Console.WriteLine("Teacher not found!");
                return;
            }

            Console.WriteLine("Get Course By Teacher ID");
            IEnumerable<Course> list = bl.GetCourseByTeacherID(teacherID);
            foreach (Course i in list)
            {
                Console.WriteLine("- " + i.CourseName);
            }
        }

        private static void ChangeTeacherOfCourse()
        {
            Console.Write("Enter Course ID: ");
            int courseID = Convert.ToInt32(Console.ReadLine());

            var course = bl.GetCourseByID(courseID);

            if (course == null)
            {
                Console.WriteLine("Course not found!");
                return;
            }

            Console.Write("Enter new teacher ID: ");
            int teacherID = Convert.ToInt32(Console.ReadLine());

            if (bl.GetTeacherByID(teacherID) == null)
            {
                Console.WriteLine("Teacher not found!");
                return;
            }

            if(teacherID == course.TeacherId)
            {
                Console.WriteLine("Same teacher ID! No changed have been made!");
                return;
            }

            course.TeacherId = teacherID;
            bl.UpdateCourse(course);

        }

        private static void CreateCourseAndTeacher()
        {
            Console.WriteLine("- CREATE COURSE AND TEACHER -");
            Console.WriteLine("Enter Course Name: ");
            string courseName = Console.ReadLine();
            Console.WriteLine("Enter Teacher Name: ");
            string teacherName = Console.ReadLine();
            Console.WriteLine("Enter Standard ID: ");
            int standardID = Convert.ToInt32(Console.ReadLine());

            if (bl.GetStandardByID(standardID) == null)
            {
                Console.WriteLine("Stanard not found!");
                return;
            }

            Teacher t1 = new Teacher
            {
                TeacherName = teacherName,
                StandardId = standardID
            };

            bl.AddTeacher(t1);

            Course c1 = new Course
            {
                TeacherId = bl.GetTeacherByName(teacherName).TeacherId,
                CourseName = courseName
            };

            bl.AddCourse(c1);
        }
    }
}

