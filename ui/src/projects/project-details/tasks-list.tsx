import { LoadingSpinner, NamedPanel } from "@root/components";
import type * as model from './project-details.model';
import projectDetailsService from './project-details.service';
import { useEffect, useState } from "react";
import styled from "@emotion/styled";

export interface TasksListProps {
    projectId: string;
}

const ListStyled = styled.ul(`
    list-style: none;
    padding-left: 0;
`);

export default function TasksList({ projectId }: TasksListProps) {
    const [loadingTasks, setLoadingTasks] = useState(false);
    const [tasks, setTasks] = useState<model.TaskData[]>([]);

    useEffect(() => {
        setLoadingTasks(true);
        setTasks([]);
        const request: model.FetchProjectTasksRequest = {
            projectId: projectId!,
        };
        projectDetailsService.fetchProjectTasks(request).then((r) => {
            setTasks(r.tasks);
            setLoadingTasks(false);
        });
    }, [projectId]);

    return (<NamedPanel title='Tasks'>
        {loadingTasks ? <LoadingSpinner /> : <ListStyled>
            {tasks.map((task) => (<li key={task.id}>{task.title}</li>))}
        </ListStyled>}
    </NamedPanel>);
}