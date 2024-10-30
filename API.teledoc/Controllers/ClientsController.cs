using Microsoft.AspNetCore.Mvc;
using Domain.ModelsDTO;
using Domain.Interfaces.Services;
using System.Threading.Tasks;
using Business.Interfaces.Services;
using WebApi.Handlers; // Добавляем ссылку на обработчик ответов

namespace API.teledoc.Controllers
{
    /// <summary>
    /// Контроллер для управления клиентами.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientServiceContainer _clientServiceContainer;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="clientServiceContainer">Контейнер сервисов для работы с клиентами.</param>
        public ClientsController(IClientServiceContainer clientServiceContainer)
        {
            _clientServiceContainer = clientServiceContainer;
        }

        /// <summary>
        /// Создание нового клиента.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Clients/Create
        ///     {
        ///        "INN": "1234567890", // длина ровна 10
        ///        "Type": "1-2",
        ///        "Address": "Москва, 3-я улица Ямского Поля, 2к26",
        ///        "Phone": "89645258390", // длина ровна 10
        ///        "Status": "1-3",
        ///        "Email": "Spektralesandrxd@gmail.com",
        ///        "dateAdded": "2024-10-30T13:55:22.909Z", // не учитывается, но есть в dto модели
        ///        "dateUpdated": "2024-10-30T13:55:22.909Z" // не учитывается, но есть в dto модели
        ///     }
        /// </remarks>
        /// <param name="dto">Объект <c>ClientDto</c> для создания нового клиента. Учитывается ссылочная целостность и валидация.</param>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ClientDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _clientServiceContainer.ClientServiceCreate.Create(dto);
            return ResponseHandler.HandleResponse(result);
        }

        /// <summary>
        /// Чтение клиентов по условию.
        /// </summary>
        /// <remarks>
        /// Примеры запроса:
        /// 
        ///     GET /Clients/ReadByCondition?INN=1234567890 // можно искать по любому уникальному полю(номер телефона, инн, почта)
        /// </remarks>
        /// <param name="dto">Объект <c>ClientDto</c> с условиями для поиска клиентов.</param>
        /// <returns>Результат операции.</returns>
        [HttpGet("ReadByCondition")]
        public async Task<IActionResult> ReadByCondition([FromQuery] ClientDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _clientServiceContainer.ClientServiceRead.Read(dto);
            return ResponseHandler.HandleResponse(result);
        }

        /// <summary>
        /// Чтение всех клиентов.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     GET /Clients/ReadAll
        /// </remarks>
        /// <returns>Результат операции.</returns>
        [HttpGet("ReadAll")]
        public async Task<IActionResult> ReadAll()
        {
            var result = await _clientServiceContainer.ClientServiceRead.ReadAll();
            return ResponseHandler.HandleResponse(result);
        }

        /// <summary>
        /// Обновить существующего клиента.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     PUT /Clients/Update
        ///     {
        ///        "INN": "1234567890", // длина ровна 10
        ///        "Type": "1-2",  
        ///        "Address": "Москва, 3-я улица Ямского Поля, 2к27",
        ///        "Phone": "89645258390",  // длина ровна 10
        ///        "Status": "1-3", 
        ///        "Email": "UpdatedEmail@gmail.com", 
        ///        "dateAdded": "2024-10-30T13:55:22.909Z", // не учитывается, но есть в dto модели
        ///        "dateUpdated": "2024-10-30T14:00:00.000Z" // не учитывается, но есть в dto модели
        ///     }
        /// </remarks>
        /// <param name="dto">Объект <c>ClientDto</c> для обновления клиента.Учитывается ссылочная целостность и валидация.</param>
        /// <returns>Результат операции.</returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ClientDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _clientServiceContainer.ClientServiceUpdate.Update(dto);
            return ResponseHandler.HandleResponse(result);
        }

        /// <summary>
        /// Удалить клиента.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     DELETE /Clients/Delete
        ///     {
        ///        "INN": "1234567890"
        ///     }
        /// </remarks>
        /// <remarks>
        /// Можно использовать любое уникальное поле (номер телефона, инн, почта)
        /// </remarks>
        /// <param name="dto">Объект <c>ClientDto</c> для удаления клиента.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] ClientDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _clientServiceContainer.ClientServiceDelete.Delete(dto);
            return ResponseHandler.HandleResponse(result);
        }
        /// <summary>
        /// Удаляет всех клиентов из базы данных.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     DELETE /Clients/DeleteAll
        /// 
        /// Удаляет все записи клиентов в базе данных.
        /// </remarks>
        /// <returns>Результат операции.</returns>
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            var result = await _clientServiceContainer.ClientServiceDelete.DeleteAll();
            return ResponseHandler.HandleResponse(result);
        }

    }
}
