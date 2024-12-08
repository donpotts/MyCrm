using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Principal;
using System.Text.Json.Serialization;
using System.Web;
using MyCrm.Shared.Blazor.Authentication;
using MyCrm.Shared.Blazor.Models;
using MyCrm.Shared.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MudBlazor;

namespace MyCrm.Shared.Blazor.Services;

public class AppService
{
    private readonly HttpClient httpClient;
    private readonly JwtAuthenticationStateProvider authenticationStateProvider;

    public AppService(
        HttpClient httpClient,
        AuthenticationStateProvider authenticationStateProvider)
    {
        this.httpClient = httpClient;
        this.authenticationStateProvider
            = authenticationStateProvider as JwtAuthenticationStateProvider
                ?? throw new InvalidOperationException();
    }

    private static async Task HandleResponseErrorsAsync(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode
            && response.StatusCode != HttpStatusCode.Unauthorized
            && response.StatusCode != HttpStatusCode.NotFound)
        {
            string? message = await response.Content.ReadFromJsonAsync<string>();
            throw new Exception(message);
        }

        response.EnsureSuccessStatusCode();
    }

    public async Task RegisterUserAsync(User registerModel)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/identity/users");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(registerModel);

        HttpResponseMessage response = await httpClient.SendAsync(request);// ("/api/Users", registerModel);

        await HandleResponseErrorsAsync(response);
    }

    public async Task AccountRegisterAsync(string userName, string emailAddress, string password, string appName)
    {
        HttpRequestMessage request = new(HttpMethod.Post, "api/account/register");
        request.Content = JsonContent.Create(new { userName, emailAddress, password, appName });
        HttpResponseMessage response = await httpClient.SendAsync(request);
        await HandleResponseErrorsAsync(response);
    }

    public async Task ChangePasswordAsync(string userName, string currentPassword, string newPassword)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, $"api/account/my-profile/change-password");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(new { currentPassword, newPassword });

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<PagedResultDto<User>> ListUsersAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/identity/users");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<User>>();
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/identity/users/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task<RoleItemsDto<RoleItems>> GetRolesByUserIdAsync(string id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/identity/users/{id}/roles");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<RoleItemsDto<RoleItems>>();
    }

    public async Task<RoleItems> GetRoleByIdAsync(string id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/identity/roles/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<RoleItems>();
    }

    public async Task<RoleItemsDto<RoleItems>> GetAllRolesAsync()
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/identity/roles/all");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<RoleItemsDto<RoleItems>>();
    }

    public async Task<RoleItemsDto<RoleItems>> ListRolesAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/identity/roles");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<RoleItemsDto<RoleItems>>();
    }

    public class RoleNames
    {
        public List<string> roleNames { get; set; }
    }

    public async Task ModifyRolesAsync(string key, IEnumerable<string> roleNames)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/identity/users/{key}/roles");
        request.Headers.Add("Authorization", $"Bearer {token}");

        RoleNames roles = new RoleNames
        {
            roleNames = roleNames.ToList()
        };

        request.Content = JsonContent.Create(roles);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task ModifyPermissionsAsync(string providerName, string providerKey, List<object> permissions)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/permission-management/permissions?providerName={providerName}&providerKey={providerKey}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        var content = new { permissions = permissions };

        request.Content = JsonContent.Create(content);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<PermissionsDto> GetPermissionsAsync(string providerName, string providerKey)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");
        HttpRequestMessage request = new(HttpMethod.Get, $"api/permission-management/permissions?providerName={providerName}&providerKey={providerKey}");

        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PermissionsDto>();
    }


    public async Task UpdateUserAsync(string id, User data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/identity/users/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task DeleteUserAsync(string id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/identity/users/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task AddRoleAsync(RoleItems roleModel)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/identity/roles");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(roleModel);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task UpdateRoleAsync(string id, RoleItems data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/identity/roles/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task DeleteRoleAsync(string id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/identity/roles/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public Uri GetUri(Uri uri, int? top = null, int? skip = null, string orderby = null)
    {
        UriBuilder uriBuilder = new UriBuilder(uri);
        NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query);

        if (top.HasValue)
        {
            nameValueCollection["MaxResultCount"] = $"{top}";
        }

        if (skip.HasValue)
        {
            nameValueCollection["SkipCount"] = $"{skip}";
        }

        if (!string.IsNullOrEmpty(orderby))
        {
            nameValueCollection["Sorting"] = orderby ?? "";
        }

        uriBuilder.Query = nameValueCollection.ToString();
        return uriBuilder.Uri;
    }


    public async Task<PagedResultDto<Customer>> ListCustomersAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/app/customer");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<Customer>>();
    }

    public async Task<Customer[]?> ListCustomerAsync()
    {
        string token = authenticationStateProvider.Token
           ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, "/api/customer");
        request.Headers.Authorization = new("Bearer", token);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Customer[]>();
    }

    public async Task<Customer?> GetCustomerByIdAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/app/customer/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Customer>();
    }

    public async Task UpdateCustomerAsync(long id, Customer data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/app/customer/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<Customer?> InsertCustomerAsync(Customer data)
    {
        data.Id = 0;
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/app/customer");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Customer>();
    }

    public async Task DeleteCustomerAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/app/customer/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }


    public async Task<PagedResultDto<Address>> ListAddressesAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/app/address");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<Address>>();
    }

    public async Task<Address[]?> ListAddressAsync()
    {
        string token = authenticationStateProvider.Token
           ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, "/api/address");
        request.Headers.Authorization = new("Bearer", token);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Address[]>();
    }

    public async Task<Address?> GetAddressByIdAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/app/address/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Address>();
    }

    public async Task UpdateAddressAsync(long id, Address data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/app/address/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<Address?> InsertAddressAsync(Address data)
    {
        data.Id = 0;
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/app/address");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Address>();
    }

    public async Task DeleteAddressAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/app/address/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }


    public async Task<PagedResultDto<ProductCategory>> ListProductCategoriesAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/app/product-category");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<ProductCategory>>();
    }

    public async Task<ProductCategory[]?> ListProductCategoryAsync()
    {
        string token = authenticationStateProvider.Token
           ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, "/api/product-category");
        request.Headers.Authorization = new("Bearer", token);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<ProductCategory[]>();
    }

    public async Task<ProductCategory?> GetProductCategoryByIdAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/app/product-category/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<ProductCategory>();
    }

    public async Task UpdateProductCategoryAsync(long id, ProductCategory data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/app/product-category/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<ProductCategory?> InsertProductCategoryAsync(ProductCategory data)
    {
        data.Id = 0;
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/app/product-category");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<ProductCategory>();
    }

    public async Task DeleteProductCategoryAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/app/product-category/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }


    public async Task<PagedResultDto<ServiceCategory>> ListServiceCategoriesAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/app/service-category");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<ServiceCategory>>();
    }

    public async Task<ServiceCategory[]?> ListServiceCategoryAsync()
    {
        string token = authenticationStateProvider.Token
           ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, "/api/service-category");
        request.Headers.Authorization = new("Bearer", token);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<ServiceCategory[]>();
    }

    public async Task<ServiceCategory?> GetServiceCategoryByIdAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/app/service-category/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<ServiceCategory>();
    }

    public async Task UpdateServiceCategoryAsync(long id, ServiceCategory data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/app/service-category/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<ServiceCategory?> InsertServiceCategoryAsync(ServiceCategory data)
    {
        data.Id = 0;
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/app/service-category");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<ServiceCategory>();
    }

    public async Task DeleteServiceCategoryAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/app/service-category/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }


    public async Task<PagedResultDto<Contact>> ListContactsAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/app/contact");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<Contact>>();
    }

    public async Task<Contact[]?> ListContactAsync()
    {
        string token = authenticationStateProvider.Token
           ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, "/api/contact");
        request.Headers.Authorization = new("Bearer", token);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Contact[]>();
    }

    public async Task<Contact?> GetContactByIdAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/app/contact/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Contact>();
    }

    public async Task UpdateContactAsync(long id, Contact data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/app/contact/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<Contact?> InsertContactAsync(Contact data)
    {
        data.Id = 0;
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/app/contact");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Contact>();
    }

    public async Task DeleteContactAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/app/contact/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }


    public async Task<PagedResultDto<Opportunity>> ListOpportunitiesAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/app/opportunity");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<Opportunity>>();
    }

    public async Task<Opportunity[]?> ListOpportunityAsync()
    {
        string token = authenticationStateProvider.Token
           ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, "/api/opportunity");
        request.Headers.Authorization = new("Bearer", token);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Opportunity[]>();
    }

    public async Task<Opportunity?> GetOpportunityByIdAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/app/opportunity/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Opportunity>();
    }

    public async Task UpdateOpportunityAsync(long id, Opportunity data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/app/opportunity/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<Opportunity?> InsertOpportunityAsync(Opportunity data)
    {
        data.Id = 0;
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/app/opportunity");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Opportunity>();
    }

    public async Task DeleteOpportunityAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/app/opportunity/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }


    public async Task<PagedResultDto<Lead>> ListLeadsAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/app/lead");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<Lead>>();
    }

    public async Task<Lead[]?> ListLeadAsync()
    {
        string token = authenticationStateProvider.Token
           ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, "/api/lead");
        request.Headers.Authorization = new("Bearer", token);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Lead[]>();
    }

    public async Task<Lead?> GetLeadByIdAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/app/lead/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Lead>();
    }

    public async Task UpdateLeadAsync(long id, Lead data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/app/lead/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<Lead?> InsertLeadAsync(Lead data)
    {
        data.Id = 0;
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/app/lead");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Lead>();
    }

    public async Task DeleteLeadAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/app/lead/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }


    public async Task<PagedResultDto<Product>> ListProductsAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/app/product");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<Product>>();
    }

    public async Task<Product[]?> ListProductAsync()
    {
        string token = authenticationStateProvider.Token
           ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, "/api/product");
        request.Headers.Authorization = new("Bearer", token);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Product[]>();
    }

    public async Task<Product?> GetProductByIdAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/app/product/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Product>();
    }

    public async Task UpdateProductAsync(long id, Product data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/app/product/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<Product?> InsertProductAsync(Product data)
    {
        data.Id = 0;
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/app/product");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Product>();
    }

    public async Task DeleteProductAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/app/product/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }


    public async Task<PagedResultDto<Service>> ListServicesAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/app/service");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<Service>>();
    }

    public async Task<Service[]?> ListServiceAsync()
    {
        string token = authenticationStateProvider.Token
           ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, "/api/service");
        request.Headers.Authorization = new("Bearer", token);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Service[]>();
    }

    public async Task<Service?> GetServiceByIdAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/app/service/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Service>();
    }

    public async Task UpdateServiceAsync(long id, Service data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/app/service/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<Service?> InsertServiceAsync(Service data)
    {
        data.Id = 0;
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/app/service");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Service>();
    }

    public async Task DeleteServiceAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/app/service/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }


    public async Task<PagedResultDto<Sale>> ListSalesAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/app/sale");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<Sale>>();
    }

    public async Task<Sale[]?> ListSaleAsync()
    {
        string token = authenticationStateProvider.Token
           ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, "/api/sale");
        request.Headers.Authorization = new("Bearer", token);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Sale[]>();
    }

    public async Task<Sale?> GetSaleByIdAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/app/sale/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Sale>();
    }

    public async Task UpdateSaleAsync(long id, Sale data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/app/sale/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<Sale?> InsertSaleAsync(Sale data)
    {
        data.Id = 0;
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/app/sale");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Sale>();
    }

    public async Task DeleteSaleAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/app/sale/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }


    public async Task<PagedResultDto<Vendor>> ListVendorsAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/app/vendor");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<Vendor>>();
    }

    public async Task<Vendor[]?> ListVendorAsync()
    {
        string token = authenticationStateProvider.Token
           ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, "/api/vendor");
        request.Headers.Authorization = new("Bearer", token);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Vendor[]>();
    }

    public async Task<Vendor?> GetVendorByIdAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/app/vendor/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Vendor>();
    }

    public async Task UpdateVendorAsync(long id, Vendor data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/app/vendor/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<Vendor?> InsertVendorAsync(Vendor data)
    {
        data.Id = 0;
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/app/vendor");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Vendor>();
    }

    public async Task DeleteVendorAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/app/vendor/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }


    public async Task<PagedResultDto<SupportCase>> ListSupportCasesAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/app/support-case");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<SupportCase>>();
    }

    public async Task<SupportCase[]?> ListSupportCaseAsync()
    {
        string token = authenticationStateProvider.Token
           ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, "/api/support-case");
        request.Headers.Authorization = new("Bearer", token);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<SupportCase[]>();
    }

    public async Task<SupportCase?> GetSupportCaseByIdAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/app/support-case/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<SupportCase>();
    }

    public async Task UpdateSupportCaseAsync(long id, SupportCase data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/app/support-case/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<SupportCase?> InsertSupportCaseAsync(SupportCase data)
    {
        data.Id = 0;
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/app/support-case");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<SupportCase>();
    }

    public async Task DeleteSupportCaseAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/app/support-case/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }


    public async Task<PagedResultDto<TodoTask>> ListTodoTasksAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/app/todo-task");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<TodoTask>>();
    }

    public async Task<TodoTask[]?> ListTodoTaskAsync()
    {
        string token = authenticationStateProvider.Token
           ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, "/api/todo-task");
        request.Headers.Authorization = new("Bearer", token);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<TodoTask[]>();
    }

    public async Task<TodoTask?> GetTodoTaskByIdAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/app/todo-task/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<TodoTask>();
    }

    public async Task UpdateTodoTaskAsync(long id, TodoTask data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/app/todo-task/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<TodoTask?> InsertTodoTaskAsync(TodoTask data)
    {
        data.Id = 0;
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/app/todo-task");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<TodoTask>();
    }

    public async Task DeleteTodoTaskAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/app/todo-task/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }


    public async Task<PagedResultDto<Reward>> ListRewardsAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/app/reward");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<Reward>>();
    }

    public async Task<Reward[]?> ListRewardAsync()
    {
        string token = authenticationStateProvider.Token
           ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, "/api/reward");
        request.Headers.Authorization = new("Bearer", token);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Reward[]>();
    }

    public async Task<Reward?> GetRewardByIdAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/app/reward/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Reward>();
    }

    public async Task UpdateRewardAsync(long id, Reward data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/app/reward/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<Reward?> InsertRewardAsync(Reward data)
    {
        data.Id = 0;
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/app/reward");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Reward>();
    }

    public async Task DeleteRewardAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/app/reward/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }


    public async Task<string?> UploadImageAsync(Stream stream, int bufferSize, string contentType)
    {
        string token = authenticationStateProvider.Token
           ?? throw new Exception("Not authorized");

        MultipartFormDataContent content = [];
        StreamContent fileContent = new(stream, bufferSize);
        fileContent.Headers.ContentType = new(contentType);
        content.Add(fileContent, "image", "image");

        HttpRequestMessage request = new(HttpMethod.Post, $"/api/image");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = content;

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<string>();
    }

    public async Task<string?> UploadImageAsync(IBrowserFile image)
    {
        using var stream = image.OpenReadStream(image.Size);

        return await UploadImageAsync(stream, Convert.ToInt32(image.Size), image.ContentType);
    }
}
