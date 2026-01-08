fromCategory("Project")
    .when({
        $init: function () {
            return {
                count: 0,
                projectIds: [],
            };
        },
        ProjectCreated: function (state, event) {
            state.count += 1;
            state.projectIds.push(event.data.projectId.value);
        },
    });