import * as Data from '../projects-list.data';
import { SmallText } from '@components/index';

interface RolesDisplayProps {
    roles: Data.ProjectRole[];
}

export function RolesDisplay({ roles }: RolesDisplayProps) {
    return <SmallText>{roles.map(role => role.name).join(', ')}</SmallText>;
}