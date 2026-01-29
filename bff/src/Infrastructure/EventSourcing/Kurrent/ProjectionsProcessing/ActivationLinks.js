fromCategory("ActivationLink")
    .foreachStream()
    .when({
        $init: function () {
            return {
                linkId: null,
                userId: null,
                linkCode: null
            };
        },
        LinkCreated: function (state, event) {
            state.linkId = event.data.linkId.value;
            state.userId = event.data.userId.value;
            state.linkCode = event.data.linkCode;
            linkTo('ActivationLinkCode-' + event.data.linkCode, event);
        }
    })
    .outputState();