pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = '1'
    }

    stages {
        stage('Restore') {
            steps {
                sh 'dotnet restore CloudComputingUTN.sln'
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet build CloudComputingUTN.sln --configuration Release --no-restore'
            }
        }
        stage('Test and Coverage') {
            steps {
                sh 'dotnet test --no-build --verbosity normal --logger:"nunit;LogFilePath=TestResults/test-results.xml"'
            }
        }
    }
    post {
        always {
            nunit failIfNoResults: true, testResultsPattern: '**/TestResults/test-results.xml'
        }
    }
}