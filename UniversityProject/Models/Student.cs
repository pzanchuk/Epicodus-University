using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace UniversityProject.Models
{
  public class Student
  {
    private string _name;
    private DateTime _enrollmentDate;
    private int _id;


    public Student (string name, DateTime enrollmentDate, int id = 0)
    {
      _name = name;
      _enrollmentDate = enrollmentDate;
      _id = id;
    }

    public string GetName()
    {
      return _name;
    }

    public DateTime GetEnrollmentDate()
    {
      return _enrollmentDate;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }

    public int GetId()
    {
      return _id;
    }

    public static List<Student> GetAllStudents()
    {
      List<Student> allStudents = new List<Student> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM students;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string name = rdr.GetString(0);
        DateTime enrollmentDate = rdr.GetDateTime(1);
        int id = rdr.GetInt32(2);
        Student newStudent = new Student(name, enrollmentDate, id);
        allStudents.Add(newStudent);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStudents;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO students (name, enrollmentdate) VALUES (@name, @enrollmentdate);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);
      MySqlParameter enrollmentdate = new MySqlParameter();
      enrollmentdate.ParameterName = "@enrollmentdate";
      enrollmentdate.Value = this._enrollmentDate;
      cmd.Parameters.Add(enrollmentdate);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
