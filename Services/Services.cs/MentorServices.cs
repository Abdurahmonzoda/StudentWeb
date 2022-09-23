using Dapper;
using Domail.Emtities;
using Domail.Response;
using Services.DateContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.cs
{
    public class MentorServices
    {
        private Context _context;
        public MentorServices(Context context)
        {
            _context = context;
        }

        public async Task<Response<Mentor>> AddMentor(Mentor mentor)
        {
            var connection = _context.CreateConnection();
            string sql = $"INSERT INTO Mentor (FirstName , LastName , Email , Phone , Address , City) VALUES (@FirstName,@LastName,@Email , @Phone , @Address , @City) RETURNING id";
            try
            {
                var response = await connection.ExecuteScalarAsync<int>(sql, new { mentor.FirstName, mentor.LastName, mentor.Email, mentor.Phone, mentor.Address, mentor.City });
                mentor.Id = response;
                return new Response<Mentor>(mentor);
            }
            catch (Exception ex)
            {
                return new Response<Mentor>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<List<Mentor>>> GetMentors()
        {
            using var connection = _context.CreateConnection();
            var sql = $"SELECT * FROM Mentor";
            try
            {
                var list = await connection.QueryAsync<Mentor>(sql);
                return new Response<List<Mentor>>(list.ToList());

            }
            catch (Exception ex)
            {
                return new Response<List<Mentor>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public async Task<Response<Mentor>> UpdateMentor(Mentor mentor)
        {
            using var connection = _context.CreateConnection();
            string sql = $"UPDATE  Mentor SET FirstName = '{mentor.FirstName}', LastName = '{mentor.LastName} , Email = '{mentor.Email},Phone = '{mentor.Phone},Address = '{mentor.Address},City = '{mentor.City}' WHERE id = '{mentor.Id}'";
            try
            {
                var response = await connection.ExecuteAsync(sql);
                return new Response<Mentor>(mentor);
            }
            catch (Exception ex)
            {
                return new Response<Mentor>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<string>> DeleteMentor(int id)
        {
            using var connection = _context.CreateConnection();

            string sql = $"DELETE FROM Mentor WHERE id = '{id}'";
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
