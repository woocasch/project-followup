import styled from '@emotion/styled';
import { theme } from '@root/theme';
import * as Data from '../projects-list.data';
import { useNavigate } from 'react-router';
import { User, ClipboardCheck } from 'lucide-react';

interface ProjectCardComponentProps {
    project: Data.ProjectListItem;
}

const ProjectCardContainer = styled.div(`
    border: 1px solid ${theme.colors.border.primary};
    border-radius: ${theme.sizes.borderRadius.small};
    padding-bottom: ${theme.sizes.spacing.small};
`);

const ProjectHeaderContainer = styled.div(`
    font-size: ${theme.sizes.typography.large};
    font-weight: ${theme.sizes.typography.bold};
    margin-bottom: ${theme.sizes.spacing.small};
    text-align: center;
    background-color: ${theme.colors.background.secondary};
    display: grid;
    grid-template-columns: 1fr auto auto;
    grid-template-rows: 1fr 1fr;
`);

const ProjectName = styled.span(`
    font-size: ${theme.sizes.typography.large};
    font-weight: ${theme.sizes.typography.bold};
    grid-row: span 2;
    display: flex;
    align-items: center;
    justify-content: center;
`);

const MembersCount = styled.span(`
    font-size: ${theme.sizes.typography.small};
    margin-left: ${theme.sizes.spacing.small};
    grid-row: 1;
    grid-column: 2;
    svg {
        height: ${theme.sizes.typography.small};
    }
`);

const TasksCount = styled.span(`
    font-size: ${theme.sizes.typography.small};
    margin-left: ${theme.sizes.spacing.small};
    grid-row: 2;
    grid-column: 2;
    svg {
        height: ${theme.sizes.typography.small};
    }
`);

const ProjectContentContainer = styled.div(`
    padding-right: ${theme.sizes.spacing.medium};
    padding-left: ${theme.sizes.spacing.medium};
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