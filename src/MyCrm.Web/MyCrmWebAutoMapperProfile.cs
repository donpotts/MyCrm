using MyCrm.Customers;
using MyCrm.Addresses;
using MyCrm.ProductCategories;
using MyCrm.ServiceCategories;
using MyCrm.Contacts;
using MyCrm.Opportunities;
using MyCrm.Leads;
using MyCrm.Products;
using MyCrm.Services;
using MyCrm.Sales;
using MyCrm.Vendors;
using MyCrm.SupportCases;
using MyCrm.TodoTasks;
using MyCrm.Rewards;
using AutoMapper;

namespace MyCrm.Web;

public class MyCrmWebAutoMapperProfile : Profile
{
    public MyCrmWebAutoMapperProfile()
    {
        CreateMap<CustomerDto, CreateUpdateCustomerDto>();
        CreateMap<AddressDto, CreateUpdateAddressDto>();
        CreateMap<ProductCategoryDto, CreateUpdateProductCategoryDto>();
        CreateMap<ServiceCategoryDto, CreateUpdateServiceCategoryDto>();
        CreateMap<ContactDto, CreateUpdateContactDto>();
        CreateMap<OpportunityDto, CreateUpdateOpportunityDto>();
        CreateMap<LeadDto, CreateUpdateLeadDto>();
        CreateMap<ProductDto, CreateUpdateProductDto>();
        CreateMap<ServiceDto, CreateUpdateServiceDto>();
        CreateMap<SaleDto, CreateUpdateSaleDto>();
        CreateMap<VendorDto, CreateUpdateVendorDto>();
        CreateMap<SupportCaseDto, CreateUpdateSupportCaseDto>();
        CreateMap<TodoTaskDto, CreateUpdateTodoTaskDto>();
        CreateMap<RewardDto, CreateUpdateRewardDto>();
        CreateMap<Pages.Customers.CreateModalModel.CreateCustomerViewModel, CreateUpdateCustomerDto>();
        CreateMap<CustomerDto, Pages.Customers.EditModalModel.EditCustomerViewModel>();
        CreateMap<Pages.Customers.EditModalModel.EditCustomerViewModel, CreateUpdateCustomerDto>();
        CreateMap<Pages.Contacts.CreateModalModel.CreateContactViewModel, CreateUpdateContactDto>();
        CreateMap<ContactDto, Pages.Contacts.EditModalModel.EditContactViewModel>();
        CreateMap<Pages.Contacts.EditModalModel.EditContactViewModel, CreateUpdateContactDto>();
        CreateMap<Pages.Leads.CreateModalModel.CreateLeadViewModel, CreateUpdateLeadDto>();
        CreateMap<LeadDto, Pages.Leads.EditModalModel.EditLeadViewModel>();
        CreateMap<Pages.Leads.EditModalModel.EditLeadViewModel, CreateUpdateLeadDto>();
        CreateMap<Pages.Vendors.CreateModalModel.CreateVendorViewModel, CreateUpdateVendorDto>();
        CreateMap<VendorDto, Pages.Vendors.EditModalModel.EditVendorViewModel>();
        CreateMap<Pages.Vendors.EditModalModel.EditVendorViewModel, CreateUpdateVendorDto>();
        CreateMap<Pages.Products.CreateModalModel.CreateProductViewModel, CreateUpdateProductDto>();
        CreateMap<ProductDto, Pages.Products.EditModalModel.EditProductViewModel>();
        CreateMap<Pages.Products.EditModalModel.EditProductViewModel, CreateUpdateProductDto>();
        CreateMap<Pages.Services.CreateModalModel.CreateServiceViewModel, CreateUpdateServiceDto>();
        CreateMap<ServiceDto, Pages.Services.EditModalModel.EditServiceViewModel>();
        CreateMap<Pages.Services.EditModalModel.EditServiceViewModel, CreateUpdateServiceDto>();
    }
}
