$(function () {
    var l = abp.localization.getResource('MyCrm');
    var createModal = new abp.ModalManager(abp.appPath + 'Addresses/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Addresses/EditModal');

    var dataTable = $('#AddressesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(myCrm.addresses.address.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('MyCrm.Addresses.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('MyCrm.Addresses.Delete'), //CHECK for the PERMISSION
                                    confirmMessage: function (data) {
                                        return l('AddressDeletionConfirmationMessage');
                                    },
                                    action: function (data) {
                                        myCrm.addresses.address
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
                    title: l('Street'),
                    data: "street",
                },
                {
                    title: l('City'),
                    data: "city",
                },
                {
                    title: l('State'),
                    data: "state",
                },
                {
                    title: l('ZipCode'),
                    data: "zipCode",
                },
                {
                    title: l('Country'),
                    data: "country",
                },
                {
                    title: l('Photo'),
                    data: "photo",
                },
                {
                    title: l('Notes'),
                    data: "notes",
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

    $('#NewAddressButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
