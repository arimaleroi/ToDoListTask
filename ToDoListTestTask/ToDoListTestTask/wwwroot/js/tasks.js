function toggleComplete(id) {
    $.ajax({
        type: "POST",
        url: `/Task/ToggleComplete/${id}`,
        success: function (data) {
            $(`#checkbox-${data.taskId}`).prop('checked', data.isCompleted);
        },
        error: function () {
            alert("AJAX toggleComplete error");
        }
    });
}

function createTask() {
    var title = $("#newTaskTitle").val();

    $.ajax({
        type: "POST",
        url: "/Task/Create",
        data: { Title: title },
        success: function (data) {
            $("#task-list").append(data);
            $("#newTaskTitle").val("");
        },
        error: function () {
            alert("AJAX create error");
        }
    });
}
function deleteTask(id) {
    $.ajax({
        type: "POST",
        url: `/Task/Delete/${id}`,
        success: function () {
            $(`#task-${id}`).remove();
        },
        error: function () {
            alert("AJAX delete error");
        }
    });
}

function editTask(id) {
    var newTitle = prompt("Write new title of task:", $(`#task-title-${id}`).text());

    if (newTitle !== null) {
        $.ajax({
            type: "POST",
            url: `/Task/UpdateTitle/${id}`,
            data: { newTitle: newTitle },
            success: function () {
                $(`#task-title-${id}`).text(newTitle);
            },
            error: function () {
                alert("AJAX editTask error");
            }
        });
    }
}
