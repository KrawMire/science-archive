import { User } from "@models/user/user";

export type GetMeResponse = {
  user: User;
  claims: string[];
}
