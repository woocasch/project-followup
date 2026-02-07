import { LoadingSpinner, NamedPanel } from "@root/components";
import type * as model from './project-details.model';
import projectDetailsService from './project-details.service';
import { useEffect, useState } from "react";
import styled from "@emotion/styled";

export interface UsersListProps {
    projectId: string;
}

const ListStyled = styled.ul(`
    list-style: none;
    padding-left: 0;
`);

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
        {loadingUsers ? <LoadingSpinner /> : <ListStyled>
            {users.map((user) => (<li key={user.id}>{user.displayName}</li>))}
        </ListStyled>}
    </NamedPanel>);
}