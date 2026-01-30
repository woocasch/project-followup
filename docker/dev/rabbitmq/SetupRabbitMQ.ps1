$commands = @(
    "rabbitmqctl await_startup",
    # Create vhost
    "rabbitmqctl add_vhost projectfollowup",
    # Create users
    "rabbitmqctl add_user api-bff api-bff",

    # Set permissions for users on vhosts
    "rabbitmqctl set_permissions -p projectfollowup admin '.*' '.*' '.*'",
    "rabbitmqctl set_permissions -p projectfollowup api-bff '.*' '.*' '.*'",

    # Some rules for naming exchanges and queues:
    # EX.<service>.<entity>.<event> for exchanges
    # QU.<service>.<consuming_entity>.<sendingentity>.<event> for queues

    # Create exchanges
    'rabbitmqadmin -u admin -p admin --vhost "projectfollowup" declare exchange name="EX.bff.user.registered" type="fanout" durable=true auto_delete=false',
    'rabbitmqadmin -u admin -p admin --vhost "projectfollowup" declare exchange name="EX.bff.activation-link.generated" type="fanout" durable=true auto_delete=false',

    # Create queues
    'rabbitmqadmin -u admin -p admin --vhost "projectfollowup" declare queue name="QU.bff.activation-link.user.registered" durable=true auto_delete=false',
    'rabbitmqadmin -u admin -p admin --vhost "projectfollowup" declare queue name="QU.bff.documents.activation-link-generated" durable=true auto_delete=false',
    # Bind queues to exchanges
    'rabbitmqadmin -u admin -p admin --vhost projectfollowup declare binding source=EX.bff.user.registered destination_type=queue destination=QU.bff.activation-link.user.registered routing_key=""""',
    'rabbitmqadmin -u admin -p admin --vhost projectfollowup declare binding source=EX.bff.activation-link.generated destination_type=queue destination=QU.bff.documents.activation-link-generated routing_key=""""'
)

foreach ($command in $commands) {
    docker exec rabbitmq.projectfollowup.dev /bin/bash -c $command
}
