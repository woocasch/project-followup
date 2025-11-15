import styled from '@emotion/styled';
import { theme } from '@root/theme';
import { useId } from 'react';
import { FieldLabel } from './headers';

export interface TextProps {
  multiline?: boolean;
  value: string;
  setValue: (newValue: string) => void;
  placeholder?: string;
  label: string;
}

const TextAreaStyled = styled.textarea(`
  background-color: ${theme.colors.primary};
  border-style: ridge;
  border-width: 1px;
  height: 5em;
`);

const InputStyled = styled.input(`
  background-color: ${theme.colors.primary};
  border-style: ridge;
  border-width: 1px;
`);

export function Text(props: TextProps) {
  const id = useId();
  const multiline = props.multiline || false;
  const placeholder = props.placeholder || props.label;
  if (multiline) {
    return (
      <>
        {props.label && <FieldLabel id={id} text={props.label} />}
        <TextAreaStyled
          id={id}
          value={props.value}
          onChange={(e) => props.setValue(e.target.value)}
          placeholder={placeholder}
        />
      </>
    );
  }

  return (
    <>
      {props.label && <FieldLabel id={id} text={props.label} />}
      <InputStyled
        id={id}
        type="text"
        value={props.value}
        onChange={(e) => props.setValue(e.target.value)}
        placeholder={placeholder}
      />
    </>
  );
}
