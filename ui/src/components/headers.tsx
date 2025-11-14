import styled from '@emotion/styled';
import { theme } from '@root/theme';

export const PageHeader = styled.h2`
    font-size: ${theme.fontSizes.xxlarge};
    text-align: center;
`;

export const SectionHeader = styled.h3`
    font-size: ${theme.fontSizes.xlarge};
    text-align: center;
`;

export interface FieldLabelProps {
  text: string;
  id: string;
}

const LabelStyled = styled.label(`
    font-weight: bold;
`);

export function FieldLabel(props: FieldLabelProps) {
  return <LabelStyled htmlFor={props.id}>{props.text}</LabelStyled>;
}
