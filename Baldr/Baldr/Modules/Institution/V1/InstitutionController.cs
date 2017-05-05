using AutoMapper;
using Baldr.Modules.Institution.V1.ApiModels;
using Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Baldr.Modules.Institution.V1
{
    [Route("v1/institutions")]
    public class InstitutionController : Controller
    {
        protected IUnitOfWork UnitOfWork;
        protected IMapper Mapper;

        public InstitutionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.UnitOfWork = unitOfWork;
            this.Mapper = mapper;
        }

        //GET 
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var institutionRepository = this.UnitOfWork.GetRepository<Baldr.Models.Institution>();

            var institution = await Task.Run( () => institutionRepository.Get(id));

            institution = new Models.Institution {
                Name = "test",
                Id = 1,
                IsActive = true,
                CreatedOn = System.DateTimeOffset.MaxValue,
                ModifiedOn = System.DateTimeOffset.MinValue
            };

            if (institution == null)
            {
                return NotFound();
            }

            var institutionResult = this.Mapper.Map<InstitutionResult>(institution);

            return Ok(institutionResult);
        }

        //GET
        //PUT - 
        //POST -
        //DELETE
        //HEAD - Headers, no Body

        // PUT - POST - Can be used for create and update
        // PUT - Used for create if and only if the entire resource is put by the client - Full Replacement - Idempotent = any operation that produces the exact same result over 1 or more calls
        // POST - Used for create. Response: 201 Created, Location Headers: fully qualified where the resource was created on the server
        //      - Used for update. Response: 200 OK. Partial update. Allows for partial updates, don't need to send the entire object over the wire. NOT Idempotent

        // MEDIA Types - Mime type
        // Data format spec and set of parsing rules.
        // Request: Accept header
        // Response: Content-Type header
        // ISO 8601 Use UTC!

        // Return Response BODY
        // GET - YES
        // POST - Should when feasable - client will get back the freshest data 

        // Content Negotiation
        // Accept Header - Header values comma delimited in order of prefernce
        // Allows client to prefer a specific mime type if the API supports

        // Linking - Resource References
        // Fundemental to scalability
        // XML has XLink, JSON doesn't
        // How to in JSON - create complex object - "directory": { "href": "http link to the associated reference"}

        // -- Reference Expansion - aka Entity Expansion, Link Expansion
        // EX. Get both account and reference directory wihout having to make two requests
        // add expand param to get reference properties back

        // add fields param to get partial resources i.e. GET /accounts/id?fields=prop1,prop2,reference(prop1)

        // Pagination
        // Collections: Query params: Offset, Limit

        // Many to Many
        // /groupMembership - i.e. an account can belong to many groups and a group can have many accounts
        // return a reference link to the account and to the group

        // Errors
        // Be as descriptive as possible
        // As much info as possible
        // 409 Conflict
        // 5 props : status - http code, code - design choice, property, message - can be passed directory to UI, developerMessage, moreInfo - full doc page

        // ID's - should be opaque - should be globally unique - avoid sequential numbers (contention). I.E. UUIDs, Url64

        // Cache
        // Etag - basically a version
        // Client - if-none-match:etag
        // Server - 304 not modified
        // SSL does not allow caching

        // Avoid sessions when possible
        // --Auth every request if necessary
        // Avoid maintaining state on the server

    }
}
