$(function () {
    var l = abp.localization.getResource('MyCrm');
    var createModal = new abp.ModalManager(abp.appPath + 'Contacts/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Contacts/EditModal');

    var dataTable = $('#ContactsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(myCrm.contacts.contact.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('MyCrm.Contacts.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('MyCrm.Contacts.Delete'), //CHECK for the PERMISSION
                                    confirmMessage: function (data) {
                                        return l('ContactDeletionConfirmationMessage');
                                    },
                                    action: function (data) {
                                        myCrm.contacts.contact
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
                    title: l('Email'),
                    data: "email",
                },
                {
                    title: l('Phone'),
                    data: "phone",
                },
                {
                    title: l('Role'),
                    data: "role",
                },
                {
                    title: l('Photo'),
                    data: "photo",
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
                    title: l('Reward'),
                    data: "reward.rewardpoints",
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

    $('#NewContactButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
