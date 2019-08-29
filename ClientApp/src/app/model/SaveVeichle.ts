import { Contact } from "./Contact";
export interface SaveVeichle {
  id: number;
  modelId: number;
  makeId: number;
  isRegistred: boolean;
  features: number[];
  contact: Contact;
}
