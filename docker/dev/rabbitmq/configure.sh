# Create vhosts
rabbitmqctl add_vhost projectfollowup-bff

# Create users
rabbitmqctl add_user api-bff api-bff

# Set permissions for users on vhosts
rabbitmqctl set_permissions -p projectfollowup-bff api-bff ".*" ".*" ".*"
