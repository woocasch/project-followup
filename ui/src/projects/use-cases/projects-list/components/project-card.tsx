import { RedirectButton } from '@root/components';
import styled from '@emotion/styled';
import { theme } from '@root/theme';
import * as Data from '../projects-list.data';
import { ProjectMemberDisplay } from './project-member-display';
import { useNavigate } from 'react-router';

interface ProjectCardComponentProps {
    project: Data.ProjectListItem;
}

const ProjectCardContainer = styled.div(`
    border: 1px solid ${theme.colors.border.primary};
    border-radius: ${theme.sizes.borderRadius.small};
    width: 400px;
    flex-shrink: 0;
`);

const ProjectHeaderContainer = styled.div(`
    font-size: ${theme.sizes.typography.large};
    font-weight: ${theme.sizes.typography.bold};
    margin-bottom: ${theme.sizes.spacing.small};
    text-align: center;
    background-color: ${theme.colors.background.secondary};
    line-height: ${theme.sizes.spacing.xlarge};
`);

const ProjectContentContainer = styled.div(`
    padding-right: ${theme.sizes.spacing.medium};
    padding-left: ${theme.sizes.spacing.medium};
    text-align: justify;
`);

const ProjectFooterContainer = styled.div(`
    font-size: ${theme.sizes.typography.large};
    font-weight: ${theme.sizes.typography.bold};
    margin-top: ${theme.sizes.spacing.small};
    background-color: ${theme.colors.background.secondary};
    display: flex;
    align-items: center;
    justify-content: center;
    &>input[type="button"] {
        margin-bottom: ${theme.sizes.spacing.small};
        margin-top: ${theme.sizes.spacing.small};
    }
`);

const ProjectMembersContainer = styled.div(`
    display: flex;
    flex-direction: column;
    gap: ${theme.sizes.spacing.small};
`);

export function ProjectCardComponent({ project }: ProjectCardComponentProps) {
    const title = project.name.length > 33 ? project.name.substring(0, 33) + '...' : project.name;
    const navigate = useNavigate();
    function OpenProject() {
        navigate(`/projects/${project.id}`);
    }

    return <ProjectCardContainer>
        <ProjectHeaderContainer onClick={OpenProject}>
            {project.name}
        </ProjectHeaderContainer>
        <ProjectContentContainer>
            <div>{project.description}</div>
            <ProjectMembersContainer>
                {project.members.map(member => (
                    <ProjectMemberDisplay key={member.id} member={member} />
                ))}
            </ProjectMembersContainer>
        </ProjectContentContainer>
        <ProjectFooterContainer>
            <RedirectButton title="Open" />
        </ProjectFooterContainer>
    </ProjectCardContainer>;
}