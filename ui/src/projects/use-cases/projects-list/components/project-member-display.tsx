import { useState } from 'react';
import styled from '@emotion/styled';
import * as Data from '../projects-list.data';
import { RolesDisplay } from './roles-display';

interface ProjectMemberDisplayProps {
    member: Data.ProjectMember;
}

const ProjectMemberContainer = styled.div(`
    display: flex;
    flex-direction: column;
    text-align: center;
`);

export function ProjectMemberDisplay({ member }: ProjectMemberDisplayProps) {
    const [expanded, setExpanded] = useState(false);

    const toggleExpanded = () => {
        setExpanded(!expanded);
    };

    const buttonText = expanded ? '−' : '+';

    return <ProjectMemberContainer>
        <div onClick={toggleExpanded}>{buttonText} {member.name}</div>
        {expanded && <RolesDisplay roles={member.roles} />}
    </ProjectMemberContainer>
}