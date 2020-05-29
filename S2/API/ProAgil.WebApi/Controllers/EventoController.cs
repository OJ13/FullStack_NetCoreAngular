using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;
using ProAgil.WebApi.Dto;
using System.IO;
using System.Net.Http.Headers;

namespace ProAgil.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _proAgilRepository;
        private readonly IMapper _mapper;
        public EventoController(IProAgilRepository proAgilRepository, IMapper mapper)
        {
            _proAgilRepository = proAgilRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _proAgilRepository.GetAllEventosAsync(true);

                var results = _mapper.Map<List<EventoDto>>(eventos);

                return Ok(results);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou - {ex.Message}");
            }
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var evento = await _proAgilRepository.GetAllEventosAsyncById(eventoId, true);

                var results = _mapper.Map<EventoDto>(evento);

                return Ok(results);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou - {ex.Message}");
            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var evento = await _proAgilRepository.GetAllEventosAsyncByTema(tema, true);
                
                var results = _mapper.Map<List<EventoDto>>(evento);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou!");
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if(file.Length > 0) {

                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, fileName.Replace("\"", "").Trim());

                    using(var stream = new FileStream(fullPath, FileMode.Create)) 
                    {
                        file.CopyTo(stream);
                    }
                } 

                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Banco de Dados Falhou {ex.Message}");
            }

            return BadRequest("Erro ao tentar realizar Upload de Imagem!");
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto evento)
        {
            try
            {
                var model = _mapper.Map<Evento>(evento);
                _proAgilRepository.Add(model);
            
                if(await _proAgilRepository.SaveChangesAsync()){
                    return Created($"/api/evento/{model.EventoId}", _mapper.Map<EventoDto>(model));
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Banco de Dados Falhou {ex.Message}");
            }

            return BadRequest();
        }

        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int eventoId, EventoDto model)
        {
            try
            {
                var evento = await _proAgilRepository.GetAllEventosAsyncById(eventoId, false);
                if(evento == null) return NotFound();

                _mapper.Map(model, evento);
                _proAgilRepository.Update(evento);

                if(await _proAgilRepository.SaveChangesAsync()){
                    return Created($"/api/evento/{model.EventoId}", _mapper.Map<EventoDto>(evento));
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou!");
            }

            return BadRequest();
        }

        [HttpDelete("{EventoId}")]
        public async Task<IActionResult> Delete(int eventoId)
        {
            try
            {
                var evento = await _proAgilRepository.GetAllEventosAsyncById(eventoId, false);
                if(evento == null) return NotFound();

                _proAgilRepository.Delete(evento);

                if(await _proAgilRepository.SaveChangesAsync())
                {
                    return Ok();
                }      
                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou!");
            }

            return BadRequest();
        }

    }
}