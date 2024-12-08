$(function () {
    var l = abp.localization.getResource('MyCrm');
    var createModal = new abp.ModalManager(abp.appPath + 'SupportCases/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'SupportCases/EditModal');

    var dataTable = $('#SupportCasesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(myCrm.supportCases.supportCase.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('MyCrm.SupportCases.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('MyCrm.SupportCases.Delete'), //CHECK for the PERMISSION
                                    confirmMessage: function (data) {
                                        return l('SupportCaseDeletionConfirmationMessage');
                                    },
                                    action: function (data) {
                                        myCrm.supportCases.supportCase
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
                    title: l('CustomerId'),
                    data: "customerId",
                },
                {
                    title: l('ProductId'),
                    data: "productId",
                },
                {
                    title: l('ServiceId'),
                    data: "serviceId",
                },
                {
                    title: l('Status'),
                    data: "status",
                },
                {
                    title: l('Description'),
                    data: "description",
                },
                {
                    title: l('CreatedDateTime'),
                    data: "createdDateTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
                    },
                },
                {
                    title: l('ModifiedDateTime'),
                    data: "modifiedDateTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
                    },
                },
                {
                    title: l('UserId'),
                    data: "userId",
                },
                {
                    title: l('FollowupDate'),
                    data: "followupDate",
                },
                {
                    title: l('Icon'),
                    data: "icon",
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

    $('#NewSupportCaseButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
