using Microsoft.AspNetCore.Mvc;
using Domain.ModelsDTO;
using Domain.Interfaces.Services;
using System.Threading.Tasks;
using Business.Interfaces.Services;
using WebApi.Handlers; // Добавляем ссылку на обработчик ответов

namespace API.teledoc.Controllers
{
    /// <summary>
    /// Контроллер для управления учредителями, который предоставляет методы для создания, обновления, чтения и удаления учредителей.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FoundersController : ControllerBase
    {
        private readonly IFounderServiceContainer _founderServiceContainer;

        /// <summary>
        /// Конструктор контроллера, который инициализирует контейнер сервисов для работы с учредителями.
        /// </summary>
        /// <param name="founderServiceContainer">Контейнер сервисов для работы с учредителями, обеспечивающий доступ к необходимым методам.</param>
        public FoundersController(IFounderServiceContainer founderServiceContainer)
        {
            _founderServiceContainer = founderServiceContainer;
        }

        /// <summary>
        /// Создание нового учредителя.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Founders/Create
        ///     {
        ///         "inn": "1234567890", // длина должна быть ровна 10
        ///         "phone": "9645258390", // длина должна быть ровна 10
        ///         "lastName": "Alexandr",
        ///         "firstName": "Spektor",
        ///         "patronymic": "",
        ///         "email": "Example@email.com",
        ///         "clientINN": "1234567890",
        ///         "dateAdded": "2024-10-30T13:55:22.909Z", // не учитывается, но есть в dto модели
        ///         "dateUpdated": "2024-10-30T13:55:22.909Z" // не учитывается, но есть в dto модели
        ///     }
        /// </remarks>
        /// <param name="founderDto">Объект <c>FounderDto</c>, содержащий данные для создания нового учредителя. Включает поля для идентификации, контактной информации и поле с номером инн клиента.</param>
        /// <returns>Возвращает результат операции создания, включая статус и возможные сообщения об ошибках.</returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] FounderDto founderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _founderServiceContainer.FounderServiceCreate.Create(founderDto);
            return ResponseHandler.HandleResponse(result);
        }

        /// <summary>
        /// Обновление существующего учредителя.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     PUT /Founders/Update
        ///     {
        ///         "inn": "1234567890", // длина должна быть ровна 10
        ///         "phone": "9645258390", // длина должна быть ровна 10
        ///         "lastName": "Alexandr",
        ///         "firstName": "Spektor",
        ///         "patronymic": "",
        ///         "email": "UpdatedEmail@email.com",
        ///         "clientINN": "1234567890",
        ///         "dateAdded": "2024-10-30T13:55:22.909Z", // не учитывается, но есть в dto модели
        ///         "dateUpdated": "2024-10-30T14:00:00.000Z" // не учитывается, но есть в dto модели
        ///     }
        /// </remarks>
        /// <param name="founderDto">Объект <c>FounderDto</c>, содержащий данные для обновления учредителя. Учитывается ссылочная целостность и валидация.</param>
        /// <returns>Возвращает результат операции обновления, включая статус и сообщения об ошибках, если таковые имеются.</returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] FounderDto founderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _founderServiceContainer.FounderServiceUpdate.Update(founderDto);
            return ResponseHandler.HandleResponse(result);
        }

        /// <summary>
        /// Чтение учредителей по заданным условиям.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     GET /Founders/ReadByCondition?inn=1234567890 // можно искать по любому уникальному полю(номер телефона, инн, почта)
        /// </remarks>
        /// <param name="dto">Объект <c>FounderDto</c> с условиями для поиска учредителей. Например, можно передать ИНН, почту или номер(любое униакальное поле) телефона для фильтрации результатов.</param>
        /// <returns>Возвращает список учредителей, соответствующих заданным условиям, или сообщения об ошибках.</returns>
        [HttpGet("ReadByCondition")]
        public async Task<IActionResult> ReadByCondition([FromQuery] FounderDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _founderServiceContainer.FounderServiceRead.Read(dto);
            return ResponseHandler.HandleResponse(result);
        }

        /// <summary>
        /// Чтение всех учредителей.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     GET /Founders/ReadAll
        /// </remarks>
        /// <returns>Возвращает список всех учредителей или сообщения об ошибках.</returns>
        [HttpGet("ReadAll")]
        public async Task<IActionResult> ReadAll()
        {
            var result = await _founderServiceContainer.FounderServiceRead.ReadAll();
            return ResponseHandler.HandleResponse(result);
        }

        /// <summary>
        /// Удаление учредителя.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     DELETE /Founders/Delete
        ///     {
        ///         "inn": "1234567890"
        ///     }
        /// </remarks>
        /// <remarks>
        /// Можно использовать любое уникальное поле (номер телефона, ИНН, почта) для идентификации учредителя.
        /// </remarks>
        /// <param name="founderDto">Объект <c>FounderDto</c>, содержащий данные для удаления учредителя. Включает уникальный идентификатор для нахождения записи.</param>
        /// <returns>Возвращает результат операции удаления, включая статус и сообщения об ошибках, если таковые имеются.</returns>
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] FounderDto founderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _founderServiceContainer.FounderServiceDelete.Delete(founderDto);
            return ResponseHandler.HandleResponse(result);
        }
        /// <summary>
        /// Удаление всех учредителей
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     DELETE /Founders/DeleteAll
        /// 
        /// Метод удаляет все записи учредителей в базе данных.
        /// </remarks>
        /// <returns>Возвращает результат операции удаления всех учредителей, включая статус и сообщения об ошибках в случае неудачи.</returns>
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            var result = await _founderServiceContainer.FounderServiceDelete.DeleteAll();
            return ResponseHandler.HandleResponse(result);
        }

    }
}
