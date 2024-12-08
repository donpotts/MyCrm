$(function () {
    var l = abp.localization.getResource('MyCrm');
    var createModal = new abp.ModalManager(abp.appPath + 'Leads/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Leads/EditModal');

    var dataTable = $('#LeadsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(myCrm.leads.lead.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('MyCrm.Leads.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('MyCrm.Leads.Delete'), //CHECK for the PERMISSION
                                    confirmMessage: function (data) {
                                        return l('LeadDeletionConfirmationMessage');
                                    },
                                    action: function (data) {
                                        myCrm.leads.lead
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
                    title: l('Source'),
                    data: "source",
                },
                {
                    title: l('Status'),
                    data: "status",
                },
                {
                    title: l('PotentialValue'),
                    data: "potentialValue",
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
                    title: l('Contact'),
                    data: "contact.name",
                },
                {
                    title: l('Opportunity'),
                    data: "opportunity.stage",
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

    $('#NewLeadButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
