import styled from '@emotion/styled';
import { PageHeader } from '@root/components';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router';
import type * as model from './project-details.model';
import projectDetailsService from './project-details.service';
import TasksList from './tasks-list';
import UsersList from './users-list';

type RouteParams = Record<'projectId', string>;

const ContentStyled = styled.div(`
  margin: auto;
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 1em;
  .description {
    grid-column: 1 / 3;}
`);

export default function ProjectDetailsPage() {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const { projectId } = useParams<RouteParams>();
  const navigate = useNavigate();

  useEffect(() => {
    if (!projectId) {
      navigate('/');
      return;
    }

    const request: model.GetProjectRequest = {
      projectId: projectId,
    };

    projectDetailsService.getProject(request).then((r) => {
      if (r.project) {
        setTitle(r.project.title);
        setDescription(r.project.description);
      }
    });
  }, [projectId, navigate]);

  if (!projectId) {
    return <p>Project not found</p>;
  }

  return (
    <div style={{ textAlign: 'center' }}>
      <PageHeader>{title}</PageHeader>
      <ContentStyled>
        <div className="description">{description}</div>
        <UsersList projectId={projectId} />
        <TasksList projectId={projectId} />
      </ContentStyled>
    </div>
  );
}
