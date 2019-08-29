import { Contact } from './Contact';
import { KeyValuePair } from './KeyValuePair';

export interface Veichle {
  id: number;
  model: KeyValuePair;
  make: KeyValuePair;
  isRegistred: boolean;
  features: KeyValuePair[];
  contact: Contact;
  lastUpdate: string;
}


