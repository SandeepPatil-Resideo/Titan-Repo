@Library("bld-jenkins-shared-libraries@master") _

// Get project/microservice name based on current build info
def projectName = getJobName()

// build
buildAspNetCore (
  projectName,                                       //imageNameWithoutTag
  'src/api/Titan.UFC.Addresses.API.csproj', // csprojPath
  '2.2',                                             // version
  'titan-ufc',                                       // namespace,
  true                                               // should be false outside of tests!
) 
 
