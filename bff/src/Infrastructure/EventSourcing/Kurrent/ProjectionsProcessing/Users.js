fromCategory("UserProfile")
    .when({
        $init: function () {
            return {
                totalUsers: 0,
                users: [],
            };
        },
        UserCreated: function (state, event) {
            state.totalUsers += 1;
            state.users.push({
                id: event.data.id.value,
                displayName: event.data.displayName,
                email: event.data.email.value,
                createdAt: event.data.createdAt
            });
        }
    });