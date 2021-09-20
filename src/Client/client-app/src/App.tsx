import {useEffect, useState} from 'react';
import IActivity from './types/common/activity.model';
import axios from 'axios';

import './App.css';
import { API_URL } from './types/common/constants';

function App() {

  const [activities, setActivities] = useState<IActivity[]>([]);

  useEffect(() => {
    axios
      .get(`${API_URL}/activities`)
      .then(response => {
          setActivities(response.data);
      });

  }, []);

  return (
    <div className="App">
      <ul>
        {
          activities.map((activity: IActivity) => (<li key={activity.id}>{activity.title}</li>))
        }
      </ul>
    </div>
  );
}

export default App;
