fromCategory("Project")
    .foreachStream()
    .when({
        $init: function () {
            return {
                id: null,
                title: null,
                description: null,
                createdAt: null,
            };
        },
        ProjectCreated: function (state, event) {
            state.id = event.data.projectId.value;
            state.title = event.data.title;
            state.description = event.data.description;
            state.createdAt = event.data.createdAt;
        }
    })
    .outputState();