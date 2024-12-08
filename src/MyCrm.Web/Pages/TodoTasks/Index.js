$(function () {
    var l = abp.localization.getResource('MyCrm');
    var createModal = new abp.ModalManager(abp.appPath + 'TodoTasks/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'TodoTasks/EditModal');

    var dataTable = $('#TodoTasksTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(myCrm.todoTasks.todoTask.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('MyCrm.TodoTasks.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('MyCrm.TodoTasks.Delete'), //CHECK for the PERMISSION
                                    confirmMessage: function (data) {
                                        return l('TodoTaskDeletionConfirmationMessage');
                                    },
                                    action: function (data) {
                                        myCrm.todoTasks.todoTask
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
                    title: l('AssignedTo'),
                    data: "assignedTo",
                },
                {
                    title: l('Status'),
                    data: "status",
                },
                {
                    title: l('DueDate'),
                    data: "dueDate",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
                    },
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
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
                    },
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

    $('#NewTodoTaskButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
