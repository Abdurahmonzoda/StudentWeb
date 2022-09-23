using Dapper;
using Services.DateContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Services;
using Domail.Response;
using Domail.Emtities;
using System.Numerics;
using System.Net;

namespace Services.Services.cs
{
    public class StudentServices
    {
        private Context _context;
        public StudentServices(Context context)
        {
            _context = context;
        }

        public async Task<Response<Student>> AddStudent(Student student)
        {
            var connection = _context.CreateConnection();
            string sql = $"INSERT INTO Student (FirstName , LastName , Email , Phone , Address , City) VALUES (@FirstName,@LastName,@Email , @Phone , @Address , @City) RETURNING id";
            try
            {
                var response = await connection.ExecuteScalarAsync<int>(sql, new { student.FirstName, student.LastName, student.Email , student.Phone , student.Address , student.City});
                student.Id = response;
                return new Response<Student>(student);
            }
            catch (Exception ex)
            {
                return new Response<Student>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<List<Student>>> GetStudents()
        {
            using var connection = _context.CreateConnection();
            var sql = $"SELECT * FROM Student";
            try
            {
                var list = await connection.QueryAsync<Student>(sql);
                return new Response<List<Student>>(list.ToList());

            }
            catch (Exception ex)
            {
                return new Response<List<Student>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }





        public async Task<Response<Student>> UpdateStudent(Student student)
        {
            using var connection = _context.CreateConnection();
            string sql = $"UPDATE  Student SET FirstName = '{student.FirstName}', LastName = '{student.LastName} , Email = '{student.Email},Phone = '{student.Phone},Address = '{student.Address},City = '{student.City}' WHERE id = '{student.Id}'";
            try
            {
                var response = await connection.ExecuteAsync(sql);
                return new Response<Student>(student);
            }
            catch (Exception ex)
            {
                return new Response<Student>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<string>> DeleteStudent(int id)
        {
            using var connection = _context.CreateConnection();

            string sql = $"DELETE FROM Student WHERE id = '{id}'";
            try
            {
                var response = await connection.ExecuteAsync(sql);
                return new Response<string>("Success");
            }
            catch (Exception ex)
            {
                return new Response<string>($"Very bad error : {ex.Message}");
            }
        }


    }
}
