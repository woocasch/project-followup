import { Button, LoadingSpinner, NamedPanel } from "@root/components";
import type * as model from './project-details.model';
import projectDetailsService from './project-details.service';
import { useEffect, useState } from "react";
import styled from "@emotion/styled";
import { theme } from "@root/theme";

export interface UsersListProps {
    projectId: string;
}

const ListStyled = styled.ul(`
    margin: ${theme.spaces.medium};
    list-style: none;
    padding-left: 0;
    display: flex;
    gap: ${theme.spaces.medium}
`);

function displayUsers(users: model.UserData[]) {
    if (users.length === 0) {
        return <p>No users assigned to this project</p>;
    }

    return <ListStyled>
        {users.map((user) => (
            <li key={user.id}>
                <Button buttonType="rounded" variant="success" title={user.displayName}>{user.displayName}</Button>
            </li>))}
    </ListStyled>;
}

export default function UsersList({ projectId }: UsersListProps) {
    const [loadingUsers, setLoadingUsers] = useState(false);
    const [users, setUsers] = useState<model.UserData[]>([]);

    useEffect(() => {
        setLoadingUsers(true);
        setUsers([]);
        const request: model.FetchProjectUsersRequest = {
            projectId: projectId!,
        };
        projectDetailsService.fetchProjectUsers(request).then((r) => {
            setUsers(r.users);
            setLoadingUsers(false);
        });
    }, [projectId]);

    return (<NamedPanel title='Assigned users'>
        {loadingUsers ? <LoadingSpinner /> : displayUsers(users)}
    </NamedPanel>);
}