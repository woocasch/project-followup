fromCategory("UserProfile")
    .foreachStream()
    .when({
        $init: function () {
            return {
                id: null,
                displayName: null,
                email: null,
                createdAt: null,
            };
        },
        UserCreated: function (state, event) {
            state.id = event.data.id.value;
            state.displayName = event.data.displayName;
            state.email = event.data.email.value;
            state.createdAt = event.data.createdAt;
            linkTo('EmailToUser-' + event.data.email.value.toLowerCase(), event);
        }
    })
    .outputState();