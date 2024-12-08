$(function () {
    var l = abp.localization.getResource('MyCrm');
    var createModal = new abp.ModalManager(abp.appPath + 'Vendors/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Vendors/EditModal');

    var dataTable = $('#VendorsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(myCrm.vendors.vendor.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('MyCrm.Vendors.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('MyCrm.Vendors.Delete'), //CHECK for the PERMISSION
                                    confirmMessage: function (data) {
                                        return l('VendorDeletionConfirmationMessage');
                                    },
                                    action: function (data) {
                                        myCrm.vendors.vendor
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
                    title: l('ContactName'),
                    data: "contactName",
                },
                {
                    title: l('Phone'),
                    data: "phone",
                },
                {
                    title: l('Email'),
                    data: "email",
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
                    title: l('Service'),
                    data: "service.name",
                },
                {
                    title: l('Product'),
                    data: "product.name",
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

    $('#NewVendorButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
