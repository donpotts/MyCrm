$(function () {
    var l = abp.localization.getResource('MyCrm');
    var createModal = new abp.ModalManager(abp.appPath + 'Customers/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Customers/EditModal');

    var dataTable = $('#CustomersTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(myCrm.customers.customer.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('MyCrm.Customers.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('MyCrm.Customers.Delete'), //CHECK for the PERMISSION
                                    confirmMessage: function (data) {
                                        return l('CustomerDeletionConfirmationMessage');
                                    },
                                    action: function (data) {
                                        myCrm.customers.customer
                                            .delete(data.record.id)
                                            .then(function() {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Name'),
                    data: "name",
                },
                {
                    title: l('Type'),
                    data: "type",
                },
                {
                    title: l('Industry'),
                    data: "industry",
                },
                {
                    title: l('Logo'),
                    data: "logo",
                },
                {
                    title: l('Notes'),
                    data: "notes",
                },
                {
                    title: l('Address'),
                    data: "address.city",
                },
                {
                    title: l('Contact'),
                    data: "contact.name",
                },
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewCustomerButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
