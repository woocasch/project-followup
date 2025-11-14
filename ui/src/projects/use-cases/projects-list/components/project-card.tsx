import styled from '@emotion/styled';
import { theme } from '@root/theme';
import * as Data from '../projects-list.data';
import { useNavigate } from 'react-router';
import { User, ClipboardCheck } from 'lucide-react';

interface ProjectCardComponentProps {
    project: Data.ProjectListItem;
}

const ProjectCardContainer = styled.div(`
`);

const ProjectHeaderContainer = styled.div(`
`);

const ProjectName = styled.span(`
`);

const MembersCount = styled.span(`
`);

const TasksCount = styled.span(`
`);

const ProjectContentContainer = styled.div(`
`);

export function ProjectCardComponent({ project }: ProjectCardComponentProps) {
    const navigate = useNavigate();
    function OpenProject() {
        navigate(`/projects/${project.id}`);
    }

    return <ProjectCardContainer>
        <ProjectHeaderContainer onClick={OpenProject}>
            <ProjectName>{project.name}</ProjectName>
            <MembersCount><User />{project.membersCount}</MembersCount>
            <TasksCount><ClipboardCheck />42</TasksCount>
        </ProjectHeaderContainer>
        <ProjectContentContainer>
            {project.description}
        </ProjectContentContainer>
    </ProjectCardContainer>;
}