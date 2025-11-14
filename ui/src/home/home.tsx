import './home.scss';
import ProjectsListComponent from '@root/projects/use-cases/projects-list/projects-list';

export default function HomePage() {
  return (
    <div>
      <ProjectsListComponent />
    </div>
  );
}
