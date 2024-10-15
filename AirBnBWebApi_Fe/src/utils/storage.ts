import _ from 'lodash';

export const storage = {
  get: (key: string): any => {
    const value = localStorage.getItem(key);
    return !_.isEmpty(value) ? JSON.parse(value!) : null;
  },
  set: (key: string, value: any): void => {
    const storedValue = _.isPlainObject(value) ? JSON.stringify(value) : value;
    localStorage.setItem(key, storedValue);
  },
  remove: (key: string): void => {
    localStorage.removeItem(key);
  },
  clear: (): void => {
    localStorage.clear();
  }
};
