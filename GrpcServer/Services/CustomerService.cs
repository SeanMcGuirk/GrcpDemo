using Grpc.Core;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        public ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();
            (string first, string last)[] names =  {("Sean", "McGuirk"),
                                                    ("Tristan", "Sorenson"), 
                                                    ("Garret", "Delaney")};

            output.FirstName = names[request.UserId].first;
            output.LastName = names[request.UserId].last;

            return Task.FromResult(output);

        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Sean",
                    LastName = "McGuirk",
                    EmailAddress = "sean@gmail.com",
                    Age = 21,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Tristan",
                    LastName = "Sorenson",
                    EmailAddress = "tristan@gmail.com",
                    Age = 22,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Garret",
                    LastName = "Delaney",
                    EmailAddress = "garret@gmail.com",
                    Age = 25,
                    IsAlive = true
                }
            };

            foreach (var customer in customers) 
            {
                await responseStream.WriteAsync(customer);
            }

        }
    }
}
