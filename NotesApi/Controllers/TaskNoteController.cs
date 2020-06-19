using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Extentions;
using NotesApi.Models;
using NotesApi.Resourse;
using NotesApi.Resourse.Save;
using NotesApi.Response.Result;
using NotesApi.Services.Interfaces;

namespace NotesAPI.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [Route("/api/[controller]")]
    public class TaskNoteController : Controller
    {
        private readonly ITaskNoteService taskNoteService;
        private readonly IMapper mapper;
        const int ITEMS_PER_PAGE = 10;
        public TaskNoteController(ITaskNoteService taskNoteService, IMapper mapper)
        {
            this.taskNoteService = taskNoteService;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ResponseResult> GetAllAsync(int id)
        {
            var taskNotes = await taskNoteService.ListAsync();
             taskNotes = taskNotes.Where(x => x.UserId == id);
            var count = (int)Math.Ceiling((decimal)taskNotes.Count() / 10);
            var result = new ResponseResult
            {
                Data = count,
                Message = "",
                Success = true
            };
            return result;
        }

        [HttpGet("{page}/{id}")]
        public async Task<ResponseResult> GetAllAsync(int page, int id)
        {
            var taskNotes = await taskNoteService.ListAsync();
            taskNotes = taskNotes.Where(x => x.UserId == id);
            var collection = taskNotes.Skip((page - 1) * ITEMS_PER_PAGE)
                                                    .Take(ITEMS_PER_PAGE);
            var pageCount = (int)Math.Ceiling((decimal)taskNotes.Count() / ITEMS_PER_PAGE);
            if (id < pageCount)
            {
                var result = new ResponseResult
                {
                    Data = null,
                    Message = "Page not Found",
                    Success = false
                };
                return result;
            }
            else
            {

                var resources = mapper.Map<IEnumerable<TaskNote>, IEnumerable<TaskNoteResourse>>(collection);
                var result = new ResponseResult
                {
                    Data = resources,
                    Message = "",
                    Success = true
                };
                return result;
            }
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveTaskNoteResourse resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var taskNote = mapper.Map<SaveTaskNoteResourse, TaskNote>(resource);
            var taskNoteResponse = await taskNoteService.SaveAsync(taskNote);
            var taskNoteResource = mapper.Map<TaskNote, TaskNoteResourse>(taskNoteResponse.TaskNote);
            var result = taskNoteResponse.GetResponseResult(taskNoteResource);
            return Ok(result);


        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTaskNoteResourse resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var taskNote = mapper.Map<SaveTaskNoteResourse, TaskNote>(resource);
            var taskNoteResponse = await taskNoteService.UpdateAsync(id, taskNote);
            var taskNoteResource = mapper.Map<TaskNote, TaskNoteResourse>(taskNoteResponse.TaskNote);
            var result = taskNoteResponse.GetResponseResult(taskNoteResource);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var taskNoteResponse = await taskNoteService.DeleteAsync(id);
            var taskNoteResource = mapper.Map<TaskNote, TaskNoteResourse>(taskNoteResponse.TaskNote);
            var result = taskNoteResponse.GetResponseResult(taskNoteResource);
            return Ok(result);
        }
    }
}