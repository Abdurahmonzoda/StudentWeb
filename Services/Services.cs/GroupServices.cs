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
    public class GroupServices
    {
            private Context _context;
            public GroupServices(Context context)
            {
                _context = context;
            }

            public async Task<Response<Group>> AddGroup(Group group)
            {
                var connection = _context.CreateConnection();
                string sql = $"INSERT INTO Group (GroupName , GroupDescription , CourseId ) VALUES (@GroupName,@GroupDescription,@CourseId)";
                try
                {
                    var response = await connection.ExecuteScalarAsync<Group>(sql, new { group.GroupName, group.GroupDescription, group.CourseId });
                    group = response;
                    return new Response<Group>(group);
                }
                catch (Exception ex)
                {
                    return new Response<Group>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
                }
            }

            public async Task<Response<List<Group>>> GetGroup()
            {
                using var connection = _context.CreateConnection();
                var sql = $"SELECT * FROM Group";
                try
                {
                    var list = await connection.QueryAsync<Group>(sql);
                    return new Response<List<Group>>(list.ToList());

                }
                catch (Exception ex)
                {
                    return new Response<List<Group>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
                }
            }


            public async Task<Response<Group>> UpdateGroup(Group group)
            {
                using var connection = _context.CreateConnection();
                string sql = $"UPDATE  Group SET GroupName = '{group.GroupName}', GroupDescription = '{group.GroupDescription} , CourseId = '{group.CourseId} WHERE id = '{group.Id}'";
                try
                {
                    var response = await connection.ExecuteAsync(sql);
                    return new Response<Group>(group);
                }
                catch (Exception ex)
                {
                    return new Response<Group>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
                }
            }

            public async Task<Response<string>> DeleteGroup(int id)
            {
                using var connection = _context.CreateConnection();

                string sql = $"DELETE FROM Group WHERE CourseId = '{id}'";
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
