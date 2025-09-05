/// Author: Brian Haynes
using Microsoft.AspNetCore.Mvc;
using TransactionApp.Application;

namespace TransactionApp.API.Controllers
{
    /// <summary>
    /// Class to handle the building of the Swagger API calls for transactions.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }
        /// <summary>
        /// Method to add a new Transaction to the database
        /// </summary>
        /// <param name="dto"><c>TransactionDto</c> object containing the data to be added</param>
        /// <returns><c>IActionResult</c> containing the new transaction added to the database.</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]TransactionDto dto)
        {
            var result = await _service.AddTransactionAsync(dto);
            return CreatedAtAction(nameof(Add), new { id = result.Id }, result);
        }
        /// <summary>
        /// Method to retrieve all Transactions from the database
        /// </summary>
        /// <returns><c>IActionResult</c> containing the list of transactions.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
        /// <summary>
        /// Method to retrieve all transactions above a certain threshold.
        /// </summary>
        /// <param name="threshold">Value for which all transactions higher than will be gathered.</param>
        /// <returns><c>IActionResult</c> containing the list of transactions.</returns>
        [HttpGet("high-volume")]
        public async Task<IActionResult> GetHighVolume([FromQuery] decimal threshold) =>
            Ok(await _service.GetHighVolumeTransactions(threshold));
        /// <summary>
        /// Method to retrieve total transactions for each user. 
        /// </summary>
        /// <returns><c>IActionResult</c> containing the list of users with their transaction values.</returns>
        [HttpGet("summary/user")]
        public async Task<IActionResult> SummaryPerUser() =>
            Ok(await _service.GetTotalAmountPerUser());
        /// <summary>
        /// Method to retrieve total transactions for each transaction type.
        /// </summary>
        ///  <returns><c>IActionResult</c> containing each transaction type with their transaction values.</returns>
        [HttpGet("summary/type")]
        public async Task<IActionResult> SummaryPerType() =>
            Ok(await _service.GetTotalAmountPerType());
    }
    /// <summary>
    /// Class to handle the building of the Swagger API calls for transactions.
    /// </summary>
    [Route("api/v1/Users")]
    public class UserController : ControllerBase
    {
        public readonly IUserService _service;

        public UserController(IUserService service) {
            _service = service; 
        }
        /// <summary>
        /// Method to add a new User to the database
        /// </summary>
        /// <param name="dto"><c>UserDto</c> object containing the data to be added</param>
        /// <returns><c>IActionResult</c> containing the new user added to the database.</returns>
        [HttpPost("add")]
        public async Task<IActionResult> CreateUser([FromBody]UserDto dto)
        {
            var result = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(CreateUser), new { id = result.Id }, result);
        }
        /// <summary>
        /// Method to retrieve all Users from the database
        /// </summary>
        /// <returns><c>IActionResult</c> containing the list of users.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
        /// <summary>
        /// Method to update user data in the database.
        /// </summary>
        /// <param name="dto"><c>UserDto</c> object containing the data to be updated</param>
        /// <returns><c>IActionResult</c> containing the user whose data was updated.</returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdatedUser([FromBody]UserDto dto)
        {
            var result = await _service.UpdateAsync(dto.Id, dto);
            return CreatedAtAction(nameof(UpdatedUser), new { id = result.Id }, result);
        }
        /// <summary>
        /// Method to delete user from database 
        /// </summary>
        /// <param name="id">User ID to be deleted.</param>
        [HttpDelete("delete")]
        public async Task DeleteUser(String id)
        {
            await _service.DeleteAsync(id);
        }
    }

}
