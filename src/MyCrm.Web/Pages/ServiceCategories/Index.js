$(function () {
    var l = abp.localization.getResource('MyCrm');
    var createModal = new abp.ModalManager(abp.appPath + 'ServiceCategories/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'ServiceCategories/EditModal');

    var dataTable = $('#ServiceCategoriesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(myCrm.serviceCategories.serviceCategory.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('MyCrm.ServiceCategories.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('MyCrm.ServiceCategories.Delete'), //CHECK for the PERMISSION
                                    confirmMessage: function (data) {
                                        return l('ServiceCategoryDeletionConfirmationMessage');
                                    },
                                    action: function (data) {
                                        myCrm.serviceCategories.serviceCategory
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
                    title: l('Description'),
                    data: "description",
                },
                {
                    title: l('Icon'),
                    data: "icon",
                },
                {
                    title: l('TaxRate'),
                    data: "taxRate",
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

    $('#NewServiceCategoryButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
